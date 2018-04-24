import React from 'react';

export default class RegistrationForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            nickName: '',
            eMail: '',
            password: ''
        };
    }

    handleSubmit(e) {
        e.preventDefault();
        var data = new FormData();
        data.append('NickName', this.state.nickName);
        data.append('Email', this.state.eMail);
        data.append('Password', this.state.password);

        var xhr = new XMLHttpRequest();
        xhr.open('post', "user/register", true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);

            if (data.successful) {
                alert(data.responseData);
                this.props.onRegistered();
            } else {
                alert(data.responseData);
            }

            
        }.bind(this);
        xhr.send(data);
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
                <input className="btn btn-info" type="submit" value="Register" />
            </form>
        );
    }    
}
RegistrationForm.displayName = 'RegistrationForm';