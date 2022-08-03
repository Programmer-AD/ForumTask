import ApiAccessor from "../ApiAccessor";

export default class UserApi{
    api
    constructor(){
        this.api=new ApiAccessor("/api/user");
    }
    async get(userId){
        return await this.api.get(`/${userId}`);
    }
    async canUseEmail(email){
        return await this.api.get(`/canUse/email/${email}`);
    }
    async canUseUserName(userName){
        return await this.api.get(`/canUse/userName/${userName}`);
    }
    async register(userName,email,password){
        return await this.api.post(`/register`,{userName,email,password});
    }
    async delete(userId){
        return await this.api.delete(`/${userId}`);
    }
    async setBanned(userId,banned){
        return await this.api.put(`/${userId}/banned/${banned}`,null);
    }
    async setRole(userId,roleName,setHasRole){
        return await this.api.put(`/${userId}/roles`,{roleName,setHasRole});
    }
    async signIn(userName,password,remember){
        return await this.api.post(`/signIn`,{userName,password,remember});
    }
    async signOut(){
        return await this.api.post("/signOut");
    }
    async getCurrent(){
        return await this.api.get("/current");
    }
}