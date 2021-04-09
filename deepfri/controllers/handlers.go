package controllers

// NOTE: UNSURE HOW BIG CONTROLLER FUNCTIONS SHOULD BE IDK

import (
	"deepfri/filters"
	"deepfri/model"
	"deepfri/util"
	"encoding/json"
	"errors"
	"fmt"
	"image/png"
	"io/ioutil"
	"net/http"
	"os"
	"strconv"
	"time"

	"github.com/julienschmidt/httprouter"
)

func getImage(w http.ResponseWriter, r *http.Request, params httprouter.Params) {
	w.Header().Set("Content-Type", "application/json; charset=UTF-8")
	id := params.ByName("id")
	idInt, err := strconv.Atoi(id)
	if err != nil {
		writeErrorResponse(w, http.StatusBadRequest, "ID Error")
		return
	}
	image, err := model.LoadImageResponse(idInt)
	if err != nil {
		writeErrorResponse(w, http.StatusBadRequest, "ID Error")
		return
	}
	response := model.ImageRequestResponse{
		Status: http.StatusOK,
		Image:  image,
	}
	writeOKResponse(w, response)
}

func getRecentImages(w http.ResponseWriter, r *http.Request, params httprouter.Params) {
	w.Header().Set("Content-Type", "application/json; charset=UTF-8")
	amount := params.ByName("amount")
	amountInt, err := strconv.Atoi(amount)
	if err != nil {
		writeErrorResponse(w, http.StatusBadRequest, "ID Error")
		return
	}
	images, err := model.LoadRecentImages(amountInt)
	if err != nil {
		writeErrorResponse(w, http.StatusBadRequest, "DB Error")
		return
	}
	response := model.ImagesRequestResponse{
		Status: http.StatusOK,
		Images: images,
	}
	writeOKResponse(w, response)
}

func saveImage(w http.ResponseWriter, r *http.Request, params httprouter.Params) {
	w.Header().Set("Content-Type", "application/json; charset=UTF-8")

	fmt.Println("File Upload Endpoint Hit")

	r.ParseMultipartForm(1 << 20) // NOTE: THIS WAS 10 << 20 should have a limit of 1mb

	filter := r.FormValue("filter")

	caption := r.FormValue("caption")

	uploadFile, _, err := r.FormFile("image")
	if err != nil {
		writeErrorResponse(w, http.StatusBadRequest, "Error Retrieving the File")
		return
	}
	defer uploadFile.Close()

	newFile, err := os.Create(util.CreateRandomPath("upload"))
	if err != nil {
		writeErrorResponse(w, http.StatusBadRequest, "Error Creating new File")
		return
	}
	defer newFile.Close()

	// read all of the contents of our uploaded file into a
	// byte array
	fileBytes, err := ioutil.ReadAll(uploadFile)
	if err != nil {
		writeErrorResponse(w, http.StatusBadRequest, "Error Reading File")
		return
	}
	// write this byte array to our temporary file
	newFile.Write(fileBytes)

	image := model.ImageResponse{
		ID:           0,
		OriginalName: newFile.Name(),
		Filter:       filter,
		Caption:      caption,
		TimeStamp:    time.Now(),
	}

	id, err := image.Save()
	if err != nil {
		writeErrorResponse(w, http.StatusBadRequest, "Error Saving file to DB")
		return
	}

	okResponse := model.UploadSuccess{
		Status: http.StatusCreated,
		ID:     id,
	}
	writeOKResponse(w, okResponse)
}

func filterImage(w http.ResponseWriter, r *http.Request, params httprouter.Params) {
	id := params.ByName("id")
	idInt, err := strconv.Atoi(id)
	if err != nil {
		writeErrorResponse(w, http.StatusBadRequest, "Couldn't parse ID")
		return
	}
	imr, err := model.LoadImageResponse(idInt)
	if err != nil {
		writeErrorResponse(w, http.StatusBadRequest, "Couldn't find ID")
		return
	}

	if len(imr.FileName) != 0 {
		okResponse := model.UploadSuccess{
			Status: http.StatusOK,
			ID:     imr.ID,
		}
		writeOKResponse(w, okResponse)
		return
	}

	file, err := os.Open(imr.OriginalName)
	if err != nil {
		writeErrorResponse(w, http.StatusBadRequest, "Couldn't find image")
		return
	}
	defer file.Close()

	img, err := png.Decode(file)
	if err != nil {
		writeErrorResponse(w, http.StatusBadRequest, "Could Not Open File")
		return
	}

	// determine from imr what filter to use
	var name string
	switch imr.Filter {
	case "blue":
		name, err = filters.BlueFilter(img)
	case "trans":
		name, err = filters.SetToTransparant(img)
	default:
		err = errors.New("Couldn't find right filter to use")
	}
	if err != nil {
		writeErrorResponse(w, http.StatusBadRequest, "Could Not Filter image")
		return
	}

	imr.FileName = name
	imr.Width = img.Bounds().Max.X
	imr.Height = img.Bounds().Max.Y

	err = imr.UpdateFilteredImage()
	if err != nil {
		writeErrorResponse(w, http.StatusBadRequest, "Could Not Update Filtered image")
		return
	}

	okResponse := model.UploadSuccess{
		Status: http.StatusAccepted,
		ID:     imr.ID,
	}

	writeOKResponse(w, okResponse)
}

func writeOKResponse(w http.ResponseWriter, m interface{}) {
	w.Header().Set("Content-Type", "application/json; charset=UTF-8")
	if err := json.NewEncoder(w).Encode(m); err != nil {
		writeErrorResponse(w, http.StatusInternalServerError, "Internal Server Error")
	}
}

// Writes the error response as a Standard API JSON response with a response code
func writeErrorResponse(w http.ResponseWriter, errorCode int, errorMsg string) {
	w.Header().Set("Content-Type", "application/json; charset=UTF-8")
	w.WriteHeader(errorCode)
	json.
		NewEncoder(w).
		Encode(model.ErrorResponse{Status: errorCode, Title: errorMsg})
}
