import ApiAccessor from "../ApiAccessor";

export default class UserApi {
    apiAccessor

    constructor() {
        this.apiAccessor = new ApiAccessor("/api/user");
    }

    get(userId) {
        return this.apiAccessor.get(`/${userId}`);
    }

    canUseEmail(email) {
        return this.apiAccessor.get(`/canUse/email/${email}`);
    }

    canUseUserName(userName) {
        return this.apiAccessor.get(`/canUse/userName/${userName}`);
    }

    register(userName, email, password) {
        return this.apiAccessor.post(`/register`, { userName, email, password });
    }

    delete(userId) {
        return this.apiAccessor.delete(`/${userId}`);
    }

    setBanned(userId, banned) {
        return this.apiAccessor.put(`/${userId}/banned/${banned}`, null);
    }
    
    setRole(userId, roleName, setHasRole) {
        return this.apiAccessor.put(`/${userId}/roles`, { roleName, setHasRole });
    }

    signIn(userName, password, remember) {
        return this.apiAccessor.post(`/signIn`, { userName, password, remember });
    }

    signOut() {
        return this.apiAccessor.post("/signOut");
    }

    getCurrent() {
        return this.apiAccessor.get("/current");
    }
}