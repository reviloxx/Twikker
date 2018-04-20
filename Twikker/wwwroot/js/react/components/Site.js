import React from 'react';
import ReactDOM from 'react-dom';
import PostBox from './PostBox';
import LoginForm from './LoginForm';
import RegistrationForm from './RegistrationForm';
import Navbar from './Navbar';
import UserProfile from './UserProfile';

export default class Site extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            user: {
                userId: -1,
                nickname: '',
                firstname: '',
                lastname: '',
                email: '',
                dateofbirth: ''
            },
            posts: [{
                comments: [],
                reactions: []
            }],
            currentPage: null
        }
    }    

    componentWillMount() {
        this.getActiveUser();
        this.getPosts();             
    }

    getActiveUser() {
        var xhr = new XMLHttpRequest();
        xhr.open('get', '/user/get', true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);            
            this.setState({
                user: data
            });
            this.setState({
                user: data
            });
        }.bind(this);
        xhr.send();
    }

    getPosts() {
        var xhr = new XMLHttpRequest();
        xhr.open('get', "/posts/get", true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            this.setState({
                currentPage: null
            });
            this.setState({
                posts: data.posts,
                currentPage: <PostBox onPostsChanged={() => this.getPosts()} activeUserId={this.state.user.userId} posts={data.posts} />
            });
        }.bind(this);
        xhr.send();
    }

    sendLogout() {
        var xhr = new XMLHttpRequest();
        xhr.open('post', '/user/logout', true);
        xhr.onload = function () {
            this.componentWillMount();
        }.bind(this);
        xhr.send();
    }
    
    onItemClickedCallback(identifier) {
        switch (identifier) {
            case "Home":
                this.setState({ currentPage: <PostBox onPostsChanged={() => this.getPosts()} activeUserId={this.state.user.userId} posts={this.state.posts} /> })
                break;
            case "Register":
                this.setState({ currentPage: <RegistrationForm onRegistered={() => this.getPosts()} /> })
                break;
            case "Logout":
                this.sendLogout();
                this.setState({ currentPage: <PostBox onPostsChanged={() => this.getPosts()} activeUserId={this.state.user.userId} posts={this.state.posts} /> })
                break;
            case "Login":
                this.setState({ currentPage: <LoginForm onLoggedIn={() => this.componentWillMount()} /> })
                break;
            case "Profile":
                this.setState({ currentPage: <UserProfile onUpdated={() => this.componentWillMount()} user={this.state.user} /> })
                break;
        }        
    }

    render() {
        return (
            <div>
                <Navbar user={this.state.user} onItemClicked={(identifier) => this.onItemClickedCallback(identifier)} />
                <div id="body-content" className="container body-content">
                    {this.state.currentPage}
                </div>
            </div>
        );
    }    
}
