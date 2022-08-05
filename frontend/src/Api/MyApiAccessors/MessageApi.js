import ApiAccessor from "../ApiAccessor";

export default class MessageApi {
    apiAccessor

    constructor() {
        this.apiAccessor = new ApiAccessor("/api/message")
    }

    getPageCount(topicId) {
        return this.apiAccessor.get(`/topic${topicId}/pageCount`);
    }

    getTopOld(topicId, page) {
        return this.apiAccessor.get(`/topic${topicId}`, { page });
    }

    send(topicId, text) {
        return this.apiAccessor.post("/", { topicId, text });
    }

    edit(messageId, newText) {
        return this.apiAccessor.put(`/${messageId}`, { newText });
    }

    delete(messageId) {
        return this.apiAccessor.delete(`/${messageId}`);
    }
}