import ApiAccessor from "../ApiAccessor";

export default class MessageApi{
    api
    constructor(){
        this.api=new ApiAccessor("/api/message")
    }
    async getPageCount(topicId){
        return await this.api.get(`/topic${topicId}/pageCount`);
    }
    async getTopOld(topicId,page){
        return await this.api.get(`/topic${topicId}`,{page});
    }
    async send(topicId, text){
        return await this.api.post("/",{topicId,text});
    }
    async edit(messageId,newText){
        return await this.api.put(`/${messageId}`,newText);
    }
    async delete(messageId){
        return await this.api.delete(`/${messageId}`);
    }
}