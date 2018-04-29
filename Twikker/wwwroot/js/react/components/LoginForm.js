import React from 'react';
import sha256 from '../../node_modules/crypto-js/sha256';
import * as ajaxhandler from '../ajax-handler';

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
        var encryptedPW = sha256(this.state.password);
        data.append('NickName', this.state.nickName);        
        data.append('Password', encryptedPW);
        ajaxhandler.ajaxRequest(data, 'user/login', this.handleResponse.bind(this));
    }

    handleResponse(response) {
        if (response.successful) {
            this.props.onLoggedIn();
        } else {
            alert("Nickname / E-Mail or Password wrong.");
            this.setState({
                nickName: '',
                password: ''
            });
        }
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
                <div className="form-group">
                    <label>Nickname or E-Mail</label>
                    <input className="form-control"
                        type="text"
                        value={this.state.nickName}
                        onChange={this.handleNickNameChange.bind(this)}
                    />
                </div>
                <div className="form-group">
                    <label>Password</label>
                    <input className="form-control"
                        type="password"
                        value={this.state.password}
                        onChange={this.handlePasswordChange.bind(this)}
                    />
                </div>
                <input className="btn btn-info" type="submit" value="Login" />
            </form>
        );
    }
}
LoginForm.displayName = 'LoginForm';