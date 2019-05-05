"""Review DAO"""
import psycopg2
import requests

SQL = """INSERT INTO reviews(user_id, number_plate, review_type, review_text, created_on)
             VALUES(%s, %s, %s, %s, %s);"""

SQL2 = """SELECT * from users where username = %s;"""

class ReviewDAO():
	"""Review DAO"""
	types = {"driving":0, "parking":5, "car":10}

	def __init__(self, user_id, number_plate, review_type, review_text, date_created):
		self.user_id = user_id
		self.number_plate = number_plate
		self.review_type = self.types[review_type]
		self.review_text = review_text
		self.date_created = date_created

	def save(self):
		"""Used to insert review into database"""
		conn = psycopg2.connect(dbname="ratedrive", user="postgres", password="", host="172.19.0.1", port="4001")
		cur = conn.cursor()
		print(self.user_id)
		cur.execute(SQL, (self.user_id, self.number_plate, self.review_type, self.review_text, self.date_created,))
		conn.commit()
		cur.close()
		conn.close()
