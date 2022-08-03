import ApiError from "../Api/ApiError"

export default class ApiAccessor{
    baseUrl
    constructor(baseUrl){
        this.baseUrl=baseUrl
    }
    async request(method,subUrl,params){
        let body=params;
        if (params!==null)
            body=JSON.stringify(body);

        let result=await fetch(this.baseUrl+subUrl,{
            method,
            body,
            headers: {
                "Content-Type": "application/json"
            }
        });
        
        let resV=null;
        try{
            resV=await result.json();
        }catch(e){
            try{
                resV=await result.text();
            }catch(e){
                
            }
        }
        if (!result.ok)
            throw new ApiError(result.status,resV);

        return resV??true;
    }
    async get(subUrl,params=null){
        let query="";
        if (params!==null)
            query="?"+new URLSearchParams(params).toString();
        return await this.request("GET",subUrl+query,null)
    }
    async post(subUrl,params=null){
        return await this.request("POST",subUrl,params);
    }
    async put(subUrl,params=null){
        return await this.request("PUT",subUrl,params);
    }
    async delete(subUrl,params=null){
        return await this.request("DELETE",subUrl,params);
    }
}