"""Review DTO"""
import json
import psycopg2

SQL = """SELECT * from users where user_id = %s;"""

class ReviewDTO():
	"""Review dto"""
	types = {0 : "Driving", 5 : "Parking", 10 : "Car"}

	def __init__(self, user_id, review_type_id, review_text, created):
		self.user_id = user_id
		self.username = ""
		self.review_type = self.types[review_type_id]
		self.review_text = review_text
		self.created = created

	def get_username(self):
		"""Used to get the username from the review user id"""
		conn = psycopg2.connect(dbname="ratedrive", user="postgres", password="", host="172.19.0.1", port="4001")
		cur = conn.cursor()
		cur.execute(SQL, (self.user_id,))
		custs = cur.fetchall()
		for cust in custs:
			self.username = cust[1]
		cur.close()
		conn.close()

	def json_serialize(self):
		"""Used to get the json version of the DTO"""
		return json.dumps(
			{
				"username": self.username,
				"type": self.review_type,
				"text": self.review_text,
				"created": self.created.strftime("%d %b %Y ")
			}
		)
