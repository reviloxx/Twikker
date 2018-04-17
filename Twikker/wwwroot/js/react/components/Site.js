import React from 'react';
import ReactDOM from 'react-dom';
import PostBox from './PostBox';
import LoginForm from './LoginForm';
import RegistrationForm from './RegistrationForm';
import Navbar from './Navbar';

export default class Site extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            activeUserId: -1,
            posts: [{ comments: [] }],
            currentPage: null
        }
    }    

    componentWillMount() {
        this.getActiveUserId();
        this.getPosts();             
    }

    getActiveUserId() {
        var xhr = new XMLHttpRequest();
        xhr.open('get', '/getActiveUserId', true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            this.setState({ activeUserId: data });
        }.bind(this);
        xhr.send();
    }

    getPosts() {
        var xhr = new XMLHttpRequest();
        xhr.open('get', "/getPosts", true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            this.setState({
                activeUserId: data.activeUserId,
                posts: data.posts,
                currentPage: <PostBox activeUserId={data.activeUserId} posts={data.posts} />
            });
        }.bind(this);
        xhr.send();
    }

    sendLogout() {
        var xhr = new XMLHttpRequest();
        xhr.open('post', '/logout', true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            this.setState({ activeUserId: data });
        }.bind(this);
        xhr.send();
    }
    
    handleLogin() {
        this.getActiveUserId();       
        this.setState({ currentPage: <PostBox activeUserId={this.state.activeUserId} posts={this.state.posts} /> })      
    }

    handleRegistration() {
        this.setState({ currentPage: <PostBox activeUserId={this.state.activeUserId} posts={this.state.posts} /> })
    }   
    
    onItemClickedCallback(identifier) {
        switch (identifier) {
            case "Home":
                this.setState({ currentPage: <PostBox activeUserId={this.state.activeUserId} posts={this.state.posts} /> })
                break;
            case "Register":
                this.setState({ currentPage: <RegistrationForm onRegistered={() => this.handleRegistration()} /> })
                break;
            case "Logout":
                this.sendLogout();
                this.setState({ currentPage: <PostBox activeUserId={this.state.activeUserId} posts={this.state.posts} /> })
                break;
            case "Login":
                this.setState({ currentPage: <LoginForm onLoggedIn={() => this.handleLogin()} /> })
                break;
        }        
    }

    render() {
        return (
            <div>
                <Navbar activeUserId={this.state.activeUserId} onItemClicked={(identifier) => this.onItemClickedCallback(identifier)} />
                <div id="body-content" className="container body-content">
                    {this.state.currentPage}
                </div>
            </div>
        );
    }    
}
