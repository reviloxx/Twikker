﻿import React from 'react';

export default class LoginForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            nickName: '',
            password: ''
        };
    }

    handleSubmit(e) {
        e.preventDefault();
        var data = new FormData();
        data.append('NickName', this.state.nickName);
        data.append('Password', this.state.password);

        var xhr = new XMLHttpRequest();
        xhr.open('post', "user/login", true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            if (data.successful) {
                this.props.onLoggedIn();
            } else {
                alert("Login failed");
                this.setState({
                    nickName: '',
                    password: ''
                });
            }
            
        }.bind(this);
        xhr.send(data);
    }

    handleNickNameChange(e) {
        this.setState({ nickName: e.target.value });
    }

    handlePasswordChange(e) {
        this.setState({ password: e.target.value });
    }

    render() {
        return (
            <form className="login-form" onSubmit={this.handleSubmit.bind(this)} >
                <input className="form-control"
                    type="text"
                    placeholder="Nickname or Email"
                    value={this.state.nickName}
                    onChange={this.handleNickNameChange.bind(this)}
                />
                <input className="form-control"
                    type="password"
                    placeholder="Password"
                    value={this.state.password}
                    onChange={this.handlePasswordChange.bind(this)}
                />
                <input type="submit" value="Login" />
            </form>
        );
    }
}