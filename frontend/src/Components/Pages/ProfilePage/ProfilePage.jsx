import React from "react";
import Api from "../../../Api/ApiUnited";
import ModalDialog from "../../Common/ModalDialog/ModalDialog.jsx";
import Button from "../../Common/Button/Button.jsx";
import NotFoundPage from "../NotFoundPage/NotFoundPage.jsx"
import css from "./style.module.css";

export default class ProfliePage extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            userData: null,
            notFound: false,

            modalText: "",
            showModal: false,
            onConfirm: null
        };


        this.handleChangeBan = this.handleChangeBan.bind(this);
        this.handleChangeRole = this.handleChangeRole.bind(this);
        this.handleDelete = this.handleDelete.bind(this);
        this.handleConfirm = this.handleConfirm.bind(this);
        this.handleCancel = this.handleCancel.bind(this);
    }

    async load(retryCount = 3) {
        try {
            const userData = await Api.user.get(this.props.match.params.profileId);

            this.setState({ userData: userData });
        } catch (error) {
            if (error.code === 404) {
                this.setState({ notFound: true });
            } else if (retryCount > 0) {
                this.load(retryCount - 1);
            }
        }
    }

    handleChangeBan() {
        const userData = this.state.userData;

        this.setState({
            showModal: true,
            modalText: `Do you really want to ${userData.isBanned ? "Unban" : "Ban"} user "${userData.userName}"?`,
            onConfirm: () => {
                Api.user.setBanned(userData.id, !userData.isBanned)
                    .then(() => this.load());
            }
        });
    }

    handleChangeRole() {
        const userNotModerator = this.state.userData.roleName.toLowerCase() === "user";

        this.setState({
            showModal: true,
            modalText: `Do you really want to set role "${userNotModerator ? "Moderator" : "User"}" to user "${this.state.userData.userName}"?`,
            onConfirm: () => {
                Api.user.setRole(this.state.userData.id, "Moderator", userNotModerator)
                    .then(() => this.load());
            }
        });
    }

    handleDelete() {
        this.setState({
            showModal: true,
            modalText: `Do you really want to delete this account (${this.state.userData.userName})?`,
            onConfirm: () => {
                Api.user.delete(this.state.userData.id)
                    .then(() => {
                        if (this.props.user?.id === this.state.userData.id) {
                            Api.user.signOut();
                        }
                        this.load();
                    });
            }
        });
    }

    handleConfirm() {
        this.state.onConfirm?.();
        this.setState({ showModal: false, modalText: "", onConfirm: null });
    }

    handleCancel() {
        this.setState({ showModal: false, modalText: "", onConfirm: null });
    }

    componentDidMount() {
        this.load();
    }

    renderButtons() {
        if (!this.props.user) {
            return null;
        }

        const currentUserRoleId = Api.getRoleId(this.props.user.roleName);
        const viewedUserRoleId = Api.getRoleId(this.state.userData.roleName);

        const isOwnPage = this.props.user.id === this.state.userData.id;
        const haveMoreRights = currentUserRoleId > viewedUserRoleId;
        const haveAdditionalRoles = currentUserRoleId > 0;
        const isAdmin = currentUserRoleId === 2;

        const buttons = [];

        if (this.props.user.id !== this.state.userData.id) {
            if (haveAdditionalRoles && haveMoreRights) {
                buttons.push(<Button onClick={this.handleChangeBan} key="ban">{this.state.userData.isBanned ? "Unban" : "Ban"}</Button>);
            }
            if (isAdmin) {
                buttons.push(<Button onClick={this.handleChangeRole} key="role">Set role "{viewedUserRoleId === 0 ? "Moderator" : "User"}"</Button>);
            }
        }

        if (isOwnPage || haveMoreRights){
            buttons.push(<Button onClick={this.handleDelete} key="delete">Delete</Button>);
        }

        return (<div>
            {buttons}
        </div>);
    }

    render() {
        if (this.state.notFound){
            return <NotFoundPage />
        }

        if (this.state.userData === null){
            return <div>Loading...</div>
        }
        
        return (<>
            <h1 className={css.title}>User profile</h1>
            <div className={css.container}>
                <h2 className={css.userName}>{this.state.userData.userName}</h2>
                <span className={css.info}>Role: {this.state.userData.roleName}</span>
                <span className={css.info}>Registred at: {new Date(this.state.userData.registerDate + "Z").toLocaleString()}</span>
                {
                    this.state.userData.isBanned ?
                        <span className={css.banned}>
                            Banned
                        </span> : <span className={css.not_banned}>
                            Not banned
                        </span>
                }
                {this.renderButtons()}
                <ModalDialog visible={this.state.showModal} onClose={this.handleCancel}>
                    <div className={css.modal_message}>
                        {this.state.modalText}
                    </div>
                    <div className={css.modal_buttons}>
                        <Button onClick={this.handleConfirm}>Yes</Button>
                        <Button onClick={this.handleCancel}>No</Button>
                    </div>
                </ModalDialog>
            </div>
        </>);
    }
}