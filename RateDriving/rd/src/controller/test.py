"""Test resource"""
import json
import falcon

class TestResource():
	"""Used to test server"""

	def on_get(self, req, resp):
		"""Test json"""
		a = "Hello, World"
		resp.body = json.dumps(a, ensure_ascii=False)
		resp.status = falcon.HTTP_200
