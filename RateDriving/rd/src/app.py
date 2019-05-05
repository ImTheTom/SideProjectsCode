import falcon

import uuid

from wsgiref import simple_server

from controller.review_types import ReviewTypeResource

from controller.signup import SignUpResource

from falcon.http_status import HTTPStatus

from controller.signin import SignInResource

from controller.review import ReviewResource

from controller.test import TestResource

from falcon_cors import CORS

cors = CORS(allow_origins_list=['http://localhost:3000', 'http://localhost:4000'], allow_all_headers=True, allow_methods_list=['GET', 'POST', 'OPTIONS'])

api = application = falcon.API(middleware=[cors.middleware])

api.add_route('/types', ReviewTypeResource())

api.add_route('/signup', SignUpResource())

api.add_route('/user', SignInResource())

api.add_route('/review', ReviewResource())

api.add_route('/', TestResource())