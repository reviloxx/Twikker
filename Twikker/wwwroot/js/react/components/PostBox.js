import React from 'react';
import PostList from './PostList';
import PostForm from './PostForm';

export default class PostBox extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            activeUserId: -1,
            data: {
                posts: [{ comments: [] }]
            }
        }
    }

    componentWillMount() {
        this.getPosts();
        console.log(this.state);
    }

    getPosts() {
        var xhr = new XMLHttpRequest();
        xhr.open('get', this.props.url, true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            console.log("getPosts" + data);
            this.setState({ data: data });
        }.bind(this);
        xhr.send();
    }

    render() {
        if (this.state.data.activeUserId > -1) {
            return (
                <div className="post-box" >
                    <h1>Latest Posts</h1>
                    <PostForm onAddedPost={() => this.getPosts()} onChangedComment={() => this.getPosts()}/>
                    <PostList onDeletedPost={() => this.getPosts()} data={this.state.data} />                    
                </div>
            );
        }
        return (
            <div className="post-box" >
                <h1>Latest Posts</h1>
                <PostList data={this.state.data} />
            </div>
        );
    }
}
