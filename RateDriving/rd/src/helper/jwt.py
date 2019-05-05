import requests

def getID(jwt):
	request = requests.post("http://172.19.0.1:4003/decode", json={'jwt': jwt})
	if request.status_code != 422:
		response = request.json()
		return response['id']
	else:
		return 0

def getJWT(user_id):
	request = requests.post("http://172.19.0.1:4003/encode", json={'id': user_id})
	return request.json()