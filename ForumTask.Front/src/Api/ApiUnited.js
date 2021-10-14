import MarkApi from "./MyApiAccessors/MarkApi";
import MessageApi from "./MyApiAccessors/MessageApi";
import TopicApi from "./MyApiAccessors/TopicApi";
import UserApi from "./MyApiAccessors/UserApi";

const Api={
    mark:new MarkApi(),
    message:new MessageApi(),
    topic:new TopicApi(),
    user:new UserApi()
};

export default Api;