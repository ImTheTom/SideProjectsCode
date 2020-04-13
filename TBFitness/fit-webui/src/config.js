const dev = {
	host: {
		NAME: "http://localhost:5000/"
	}
};

const prod = {
	host: {
		NAME: "https://fitnessapi.tombowyer.com/"
	}
};

const config = process.env.ENVIRONMENT === "development" ? dev : prod;

export default {
	...config
};
