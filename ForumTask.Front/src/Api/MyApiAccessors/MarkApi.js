import ApiAccessor from "../ApiAccessor";

export default class MarkApi{
    api
    constructor(){
        this.api=new ApiAccessor("/api/mark")
    }
    async getMark(messageId){
        return await this.api.get(`/${messageId}`);
    }
    async setMark(messageId,value){
        return await this.api.put(`/${messageId}`,value);
    }
}