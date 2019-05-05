"""ReviewTypeResource"""
import json

import psycopg2

import falcon

class ReviewTypeResource():
	"""Basic resource used to gather review types"""

	def on_get(self, req, resp):
		"""Used to get the review types"""
		conn = psycopg2.connect(dbname="ratedrive", user="postgres", password="", host="172.19.0.1", port="4001")
		cur = conn.cursor()
		cur.execute("SELECT review_type_id, review_type_string FROM review_type")

		types = {}

		rows = cur.fetchall()
		for row in rows:
			types[row[0]] = row[1]

		cur.close()
		conn.close()

		resp.body = json.dumps(types, ensure_ascii=False)
		resp.status = falcon.HTTP_200
