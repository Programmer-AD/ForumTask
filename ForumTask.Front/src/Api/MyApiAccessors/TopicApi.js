import ApiAccessor from "../ApiAccessor";

export default class TopicApi{
    api
    constructor(){
        this.api=new ApiAccessor("/api/topic")
    }
    async get(topicId){
        return await this.api.get(`/${topicId}`);
    }
    async getPageCount(){
        return await this.api.get("/pageCount");
    }
    async getTopNew(page,searchTitle=""){
        let par={page};
        if (searchTitle!=="")
            par.searchTitle=searchTitle;
        return await this.api.get("/",par);
    }
    async create(title,message){
        return await this.api.post("/",{title,message});
    }
    async rename(topicId,newTitle){
        return await this.api.put(`/${topicId}`,{newTitle});
    }
    async delete(topicId){
        return await this.api.delete(`/${topicId}`);
    }
}