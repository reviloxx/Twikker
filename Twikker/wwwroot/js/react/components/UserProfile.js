import React from 'react';
import * as ajaxhandler from '../ajax-handler';

export default class UserProfile extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            nickname: this.props.user.nickName,
            firstname: this.props.user.firstName,
            lastname: this.props.user.lastName,
            email: this.props.user.email,
            dateofbirth: this.props.user.dateOfBirth
        };
    }

    handleSubmit(e) {
        e.preventDefault();
        var data = new FormData();
        data.append('NickName', this.state.nickname);
        data.append('FirstName', this.state.firstname);
        data.append('LastName', this.state.lastname);
        data.append('Email', this.state.email);
        data.append('DateOfBirth', this.state.dateofbirth);

        ajaxhandler.ajaxRequest(data, 'user/update', this.handleResponse.bind(this));
    }

    handleResponse(response) {
        if (response.successful) {
            this.props.onUpdated();
            this.alertSuccess("User profile updated!");            
        } else {
            this.alertDanger(response.responseData);
        }
    }

    handleNickNameChange(e) {
        this.setState({ nickname: e.target.value });
    }

    handleFirstNameChange(e) {
        this.setState({ firstname: e.target.value });
    }

    handleLastNameChange(e) {
        this.setState({ lastname: e.target.value });
    }

    handleEmailChange(e) {
        this.setState({ email: e.target.value });
    }

    handleDateOfBirthChange(e) {
        this.setState({ dateofbirth: e.target.value });
    }

    alertDanger(message) {
        $(".alert-danger").html(message);
        $(".alert-danger").fadeIn(100);
        setTimeout(function () {
            $(".alert-danger").fadeOut(100);
        }, 5000);
    }

    alertSuccess(message) {
        $(".alert-success").html(message);
        $(".alert-success").fadeIn(100);
        setTimeout(function () {
            $(".alert-success").fadeOut(100);
        }, 3000);
    }

    render() {
        return (
            <form className="userprofile-form" onSubmit={this.handleSubmit.bind(this)} >
                <div className="form-group">
                    <label>Nickname</label>
                    <input className="form-control"
                        type="text"
                        value={this.state.nickname}
                        onChange={this.handleNickNameChange.bind(this)}
                    />
                </div>
                <div className="form-group">
                    <label>Firstname</label>
                    <input className="form-control"
                        type="text"
                        value={this.state.firstname}
                        onChange={this.handleFirstNameChange.bind(this)}
                    />
                </div>
                <div className="form-group">
                    <label>Lastname</label>
                    <input className="form-control"
                        type="text"
                        value={this.state.lastname}
                        onChange={this.handleLastNameChange.bind(this)}
                    />
                </div>
                <div className="form-group">
                    <label>E-Mail</label>
                    <input className="form-control"
                        type="text"
                        value={this.state.email}
                        onChange={this.handleEmailChange.bind(this)}
                    />
                </div>
                <input className="btn btn-info" type="submit" value="Update Profile" />
                <div className="alert alert-danger" role="alert" />
                <div className="alert alert-success" role="alert" />
            </form>
        );
    }    
}
UserProfile.displayName = 'UserProfile';