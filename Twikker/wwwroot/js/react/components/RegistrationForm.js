import React from 'react';
import sha256 from '../../node_modules/crypto-js/sha256';
import * as ajaxhandler from '../ajax-handler';

export default class RegistrationForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            nickName: '',
            eMail: '',
            password: '',
            passwordRepeat: ''
        };
    }

    handleSubmit(e) {
        e.preventDefault();

        if (this.state.password !== this.state.passwordRepeat) {
            alert("The passwords must match!");
            this.setState({
                password: '',
                passwordRepeat: ''
            });
            return;
        }

        var data = new FormData();
        var encryptedPW = sha256(this.state.password);
        data.append('NickName', this.state.nickName);
        data.append('Email', this.state.eMail);        
        data.append('Password', encryptedPW);
        ajaxhandler.ajaxRequest(data, 'user/register', this.handleResponse.bind(this));
    }

    handleResponse(response) {
        if (response.successful) {
            alert(response.responseData);
            this.props.onRegistered();
        } else {
            alert(response.responseData);
        }
    }

    handleNickNameChange(e) {
        this.setState({ nickName: e.target.value });
    }

    handleEMailChange(e) {
        this.setState({ eMail: e.target.value });
    }

    handlePasswordChange(e) {
        this.setState({ password: e.target.value });
    }

    handlePasswordRepeatChange(e) {
        this.setState({ passwordRepeat: e.target.value });
    }

    render() {
        return (            
            <form className="registration-form" onSubmit={this.handleSubmit.bind(this)} >
                <div className="form-group">
                    <label>Nickname</label>
                    <input className="form-control"
                        type="text"
                        value={this.state.nickName}
                        onChange={this.handleNickNameChange.bind(this)}
                    />
                </div>
                <div className="form-group">
                    <label>E-Mail</label>
                    <input className="form-control"
                        type="text"
                        value={this.state.eMail}
                        onChange={this.handleEMailChange.bind(this)}
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
                <div className="form-group">
                    <label>Repeat Password</label>
                    <input className="form-control"
                        type="password"
                        value={this.state.passwordRepeat}
                        onChange={this.handlePasswordRepeatChange.bind(this)}
                    />
                </div>
                <input className="btn btn-info" type="submit" value="Register" />
            </form>
        );
    }    
}
RegistrationForm.displayName = 'RegistrationForm';