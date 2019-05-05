"""SignIn resource"""
import json
import falcon
from models.DTO.SignInCustomerDTO import SignInCustomerDTO
from helper import jwt

class SignInResource():
	"""Used to sign in users"""

	def on_post(self, req, resp):
		"""Used to access user database"""
		data = req.bounded_stream.read()

		a = json.loads(data)

		user = SignInCustomerDTO(a["email"], a["password"])
		user.fetchSaltAndPassword()
		user.generateHashedPassword()
		b = user.verifyPassword()

		if b == False:
			resp.status = falcon.HTTP_401
			resp.location = '/'
			resp.body = "Unauthorized"
			return

		r = jwt.getJWT(user.user_id)

		resp.status = falcon.HTTP_201
		resp.body = json.dumps(r)
		resp.location = '/'
