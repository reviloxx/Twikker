import React from 'react';
import PostList from './PostList';
import PostForm from './PostForm';

export default class PostBox extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            activeUserId: this.props.activeUserId,
            posts: this.props.posts            
        }
    }    

    componentWillReceiveProps() {
        this.state = {
            activeUserId: this.props.activeUserId,
            posts: this.props.posts            
        }
    }

    render() {
        if (this.state.activeUserId > -1) {
            return (
                <div className="post-box" >
                    <h1>Latest Posts</h1>
                    <PostForm onAddedPost={this.props.onPostsChanged} />
                    <PostList onDeletedPost={this.props.onPostsChanged} onChangedComment={this.props.onPostsChanged} activeUserId={this.state.activeUserId} posts={this.state.posts} />                    
                </div>
            );
        }
        return (
            <div className="post-box" >
                <h1>Latest Posts</h1>
                <PostList activeUserId={this.state.activeUserId} posts={this.state.posts} />
            </div>
        );
    }
}
