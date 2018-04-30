import React from 'react';
import ReactDOM from 'react-dom';
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
            this.alertDanger('Nickname / E - Mail or Password wrong.');
        }
    }

    handleNickNameChange(e) {
        this.setState({ nickName: e.target.value });
    }

    handlePasswordChange(e) {
        this.setState({ password: e.target.value });
    }

    alertDanger(message) {
        $(".alert").html(message);
        $(".alert").fadeIn(100);
        setTimeout(function () {
            $(".alert").fadeOut(100);
        }, 5000);
    }

    render() {
        return (
            <div>                               
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
                    <div className="alert alert-danger" role="alert"/>
                </form>
                
            </div>
        );
    }
}
LoginForm.displayName = 'LoginForm';