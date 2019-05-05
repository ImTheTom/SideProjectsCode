"""Signup Resource"""
import json
import datetime
import falcon
from models.DAO.NewCustomerDAO import NewCustomerDAO

class SignUpResource():
	"""Used to sign up users"""

	def on_post(self, req, resp):
		"""Used to access users database"""
		data = req.bounded_stream.read()

		data = json.loads(data)

		user = NewCustomerDAO(data["username"], data["email"], data["salt"], data["password"], datetime.datetime.now())

		a = user.verify()

		if a == False:
			resp.status = falcon.HTTP_401
			resp.location = '/'
			resp.body = "Unauthorized"
			return

		try:
			user.save()
		except:
			resp.status = falcon.HTTP_401
			resp.location = '/'
			resp.body = "Unauthorized"
			return

		resp.status = falcon.HTTP_201
		resp.location = '/'
