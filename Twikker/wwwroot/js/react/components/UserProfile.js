import React from 'react';

export default class UserProfile extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            nickname: this.props.user.nickName,
            firstname: this.props.user.firstname,
            lastname: this.props.user.lastname,
            email: this.props.user.email,
            dateofbirth: this.props.user.dateofbirth,
        };

        console.log(this.props.user);
    }

    handleSubmit(e) {
        e.preventDefault();
        var data = new FormData();
        data.append('NickName', this.state.nickname);
        data.append('FirstName', this.state.firstname);
        data.append('LastName', this.state.lastname);
        data.append('Email', this.state.email);
        data.append('DateOfBirth', this.state.dateofbirth);

        var xhr = new XMLHttpRequest();
        xhr.open('post', "user/update", true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);

            if (data.successful) {
                alert("User profile updated!");
                this.props.onUpdated();
            } else {
                alert(data.responseData);
            }


        }.bind(this);
        xhr.send(data);
    }

    handleNickNameChange(e) {
        this.setState({ nickname: e.target.value });
    }

    handleFirstNameChange(e) {
        this.setState({ firstname: e.target.value });
    }

    handleLastNameChange(e) {
        this.setState({ firstname: e.target.value });
    }

    handleEmailChange(e) {
        this.setState({ email: e.target.value });
    }

    handleDateOfBirthChange(e) {
        this.setState({ dateofbirth: e.target.value });
    }

    render() {
        return (
            <form className="userprofile-form" onSubmit={this.handleSubmit.bind(this)} >
                <input className="form-control"
                    type="text"
                    placeholder="Nickname"
                    value={this.state.nickname}
                    onChange={this.handleNickNameChange.bind(this)}
                />
                <input className="form-control"
                    type="text"
                    placeholder="Firstname"
                    value={this.state.firstname}
                    onChange={this.handleFirstNameChange.bind(this)}
                />
                <input className="form-control"
                    type="text"
                    placeholder="Lastname"
                    value={this.state.lastname}
                    onChange={this.handleLastNameChange.bind(this)}
                />
                <input className="form-control"
                    type="text"
                    placeholder="E-Mail"
                    value={this.state.email}
                    onChange={this.handleEmailChange.bind(this)}
                />
                <input className="form-control"
                    type="text"
                    placeholder="Date of birth"
                    value={this.state.dateofbirth}
                    onChange={this.handleDateOfBirthChange.bind(this)}
                />
                <input type="submit" value="Update" />
            </form>
        );
    }    
}