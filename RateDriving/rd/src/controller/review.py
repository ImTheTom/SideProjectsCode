"""Review Resource"""
import datetime
import json
import psycopg2
import falcon
from models.DTO.ReviewDTO import ReviewDTO
from models.DAO.ReviewDAO import ReviewDAO
from helper import jwt

SQL = """SELECT * FROM reviews where number_plate = %s FETCH FIRST 20 ROWS ONLY;"""

class ReviewResource():
	"""Used for reviews"""

	def on_get(self, req, resp):
		"""Used to get reviews"""
		for value in req.params.items():
			plate = value

		conn = psycopg2.connect(dbname="ratedrive", user="postgres", password="", host="172.19.0.1", port="4001")
		cur = conn.cursor()
		cur.execute(SQL, (plate[1], ))
		review_array = []
		rows = cur.fetchall()

		for row in rows:
			new_review = ReviewDTO(row[1], row[3], row[4], row[5])
			new_review.get_username()
			review_array.append(new_review.json_serialize())

		cur.close()
		conn.close()

		resp.body = json.dumps(review_array, ensure_ascii=False)
		resp.status = falcon.HTTP_200

	def on_post(self, req, resp):
		"""Used to submit reviews to the database"""
		data = req.bounded_stream.read()
		data = json.loads(data)

		user_id = jwt.getID(data["jwt"])

		if user_id == 0:
			resp.status = falcon.HTTP_401
			resp.location = '/'
			resp.body = "Unauthorized"
			return

		review = ReviewDAO(user_id, data["numberplate"], data["type"], data["review"], datetime.datetime.now())
		review.save()

		resp.status = falcon.HTTP_201
		resp.location = '/'
