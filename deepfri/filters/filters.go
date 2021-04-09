package filters

import (
	"deepfri/util"
	"image"
	"image/color"
	"image/draw"
)

var (
	lightBlueTrans = color.RGBA{135, 206, 250, 127}
)

// BlueFilter ...
func BlueFilter(im image.Image) (string, error) {
	trans := util.CreateImage(im)

	for x := 0; x < im.Bounds().Max.X; x++ {
		for y := 0; y < im.Bounds().Max.Y; y++ {
			trans.Set(x, y, lightBlueTrans)
		}
	}

	final := util.CreateImage(im)

	draw.Draw(final, im.Bounds(), im, image.ZP, draw.Src)
	draw.Draw(final, trans.Bounds(), trans, image.ZP, draw.Over)

	return util.ProcessImage(final)
}

// SetToTransparant ...
func SetToTransparant(im image.Image) (string, error) {
	newImage := util.CreateImage(im)

	for x := 0; x < im.Bounds().Max.X; x++ {
		for y := 0; y < im.Bounds().Max.Y; y++ {
			colourAt := im.At(x, y)
			r, g, b, _ := colourAt.RGBA()
			newImage.Set(x, y, color.RGBA{uint8(r), uint8(g), uint8(b), 127})
		}
	}

	return util.ProcessImage(newImage)
}
