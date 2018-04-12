import React from 'react';
import Post from './Post';

export default class PostList extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            posts: this.props.data.posts
        }
    }

    render() {
        console.log(this.props.data);

        var postNodes = this.props.data.posts.map(function (post) {
            return (
                <Post postId={post.postId} onDeletedPost={this.props.onDeletedPost} creator={post.creatorNickname} creatorId={post.creatorId} creationDate={post.creationDate} activeUserId={this.props.data.activeUserId} key={post.postId} >
                    {post.content}
                </Post>
            );
        }.bind(this));
        return (
            <div className="post-list">
                {postNodes}
            </div>
        );
    }
}