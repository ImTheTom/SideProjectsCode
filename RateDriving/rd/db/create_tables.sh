#!/bin/bash
psql -U postgres ratedrive -a -f users.sql
psql -U postgres ratedrive -a -f reviews.sql
psql -U postgres ratedrive -a -f review_types.sql
