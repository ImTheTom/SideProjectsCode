package model

import (
	"deepfri/database"
	"time"
)

// ImageResponse general image response
type ImageResponse struct {
	ID           int       `json:"id"`
	OriginalName string    `json:"original_name"`
	FileName     string    `json:"file_name"`
	Filter       string    `json:"filter"`
	Caption      string    `json:"caption"`
	URL          string    `json:"url"`
	Width        int       `json:"width"`
	Height       int       `json:"height"`
	TimeStamp    time.Time `json:"created_on"`
}

// ImageResponseFromMap ...
func ImageResponseFromMap(m map[string]interface{}) ImageResponse {
	var im ImageResponse
	im.FromMap(m)
	return im
}

// ToMap ...
func (im *ImageResponse) ToMap() map[string]interface{} {
	m := map[string]interface{}{
		"images_id":            im.ID,
		"images_original_name": im.OriginalName,
		"images_file_name":     im.FileName,
		"images_filter":        im.Filter,
		"images_caption":       im.Caption,
		"images_url":           im.URL,
		"images_width":         im.Width,
		"images_height":        im.Height,
		"images_timestamp":     im.TimeStamp,
	}
	return m
}

// Save ...
func (im *ImageResponse) Save() (int, error) {
	return database.InsertImage(im.ToMap())
}

// UpdateFilteredImage ...
func (im *ImageResponse) UpdateFilteredImage() error {
	return database.UpdateProcessedImage(im.FileName, im.Width, im.Height, im.ID)
}

// FromMap ...
func (im *ImageResponse) FromMap(m map[string]interface{}) {
	im.ID = int(m["images_id"].(int64))
	im.OriginalName = m["images_original_name"].(string)
	im.FileName = m["images_file_name"].(string)
	im.Filter = m["images_filter"].(string)
	im.Caption = m["images_caption"].(string)
	im.URL = m["images_url"].(string)
	im.Height = int(m["images_width"].(int64))
	im.Width = int(m["images_height"].(int64))
	im.TimeStamp = m["images_timestamp"].(time.Time)
}

// LoadImageResponse ...
func LoadImageResponse(imageID int) (*ImageResponse, error) {
	row, err := database.RetrieveImage(imageID)
	if err != nil {
		return nil, err
	}
	image := ImageResponseFromMap(row)
	return &image, nil
}

// LoadRecentImages ...
func LoadRecentImages(amount int) ([]*ImageResponse, error) {
	rows, err := database.RetrieveRecentImages(amount)
	if err != nil {
		return nil, err
	}

	var images []*ImageResponse
	for _, rowmap := range rows {
		var image ImageResponse
		image.FromMap(rowmap)
		images = append(images, &image)
	}
	return images, nil
}
