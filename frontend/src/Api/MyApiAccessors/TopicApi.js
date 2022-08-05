import ApiAccessor from "../ApiAccessor";

export default class TopicApi {
    apiAccessor

    constructor() {
        this.apiAccessor = new ApiAccessor("/api/topic")
    }

    get(topicId) {
        return this.apiAccessor.get(`/${topicId}`);
    }

    getPageCount() {
        return this.apiAccessor.get("/pageCount");
    }

    getTopNew(page, searchTitle = "") {
        let par = { page };

        if (searchTitle !== "") {
            par.searchTitle = searchTitle;
        }

        return this.apiAccessor.get("/", par);
    }

    create(title, message) {
        return this.apiAccessor.post("/", { title, message });
    }

    rename(topicId, newTitle) {
        return this.apiAccessor.put(`/${topicId}`, { newTitle });
    }

    delete(topicId) {
        return this.apiAccessor.delete(`/${topicId}`);
    }
}