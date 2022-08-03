export default class ApiError{
    code
    data
    constructor(code, data){
        this.code=code;
        this.data=data;
    }
}