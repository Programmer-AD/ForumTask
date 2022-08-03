import ApiAccessor from "../ApiAccessor";

export default class MarkApi {
    apiAccessor

    constructor() {
        this.apiAccessor = new ApiAccessor("/api/mark")
    }

    getMark(messageId) {
        return this.apiAccessor.get(`/${messageId}`);
    }

    setMark(messageId, value) {
        return this.apiAccessor.put(`/${messageId}/${value}`, null);
    }
}