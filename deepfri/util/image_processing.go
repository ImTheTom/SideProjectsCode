package util

import (
	"image"
	"image/png"
	"os"
)

// CreateImage ...
func CreateImage(im image.Image) *image.RGBA {
	upLeft := image.Point{0, 0}
	lowRight := image.Point{im.Bounds().Max.X, im.Bounds().Max.Y}
	return image.NewRGBA(image.Rectangle{upLeft, lowRight})
}

// ProcessImage ...
func ProcessImage(im image.Image) (string, error) {
	f, _ := os.Create(CreateRandomPath("processed"))
	defer f.Close()
	err := png.Encode(f, im)
	return f.Name(), err
}
