package model

// UploadSuccess ...
type UploadSuccess struct {
	Status int `json:"status"`
	ID     int `json:"title"`
}

// ErrorResponse ...
type ErrorResponse struct {
	Status int    `json:"status"`
	Title  string `json:"title"`
}

// ImageRequestResponse ...
type ImageRequestResponse struct {
	Status int            `json:"status"`
	Image  *ImageResponse `json:"image"`
}

// ImagesRequestResponse ...
type ImagesRequestResponse struct {
	Status int              `json:"status"`
	Images []*ImageResponse `json:"images"`
}
