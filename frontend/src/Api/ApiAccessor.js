import ApiError from "../Api/ApiError"

export default class ApiAccessor {
    baseUrl

    constructor(baseUrl) {
        this.baseUrl = baseUrl
    }

    async request(method, subUrl, params) {
        let body = params;

        if (params !== null) {
            body = JSON.stringify(body);
        }

        let result = await fetch(this.baseUrl + subUrl, {
            method,
            body,
            headers: {
                "Content-Type": "application/json"
            }
        });

        let resultValue = null;
        try {
            resultValue = await result.json();
        } catch (e) {
            try {
                resultValue = await result.text();
            } catch (e) {

            }
        }
        if (!result.ok) {
            throw new ApiError(result.status, resultValue);
        }

        return resultValue ?? true;
    }

    get(subUrl, params = null) {
        let query = "";

        if (params !== null) {
            query = "?" + new URLSearchParams(params).toString();
        }

        return this.request("GET", subUrl + query, null)
    }

    post(subUrl, params = null) {
        return this.request("POST", subUrl, params);
    }

    put(subUrl, params = null) {
        return this.request("PUT", subUrl, params);
    }

    delete(subUrl, params = null) {
        return this.request("DELETE", subUrl, params);
    }
}