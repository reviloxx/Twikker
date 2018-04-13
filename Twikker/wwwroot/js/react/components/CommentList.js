import React from 'react';
import Post from './Post';
import Comment from './Comment';

export default class CommentList extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            comments: this.props.comments
        }
    }

    render() {
        console.log('Comments in CommentList: ' + this.props.comments);

        return (<div />);
        var commentNodes = this.props.comments.map(function (comment) {
            return (
                <Comment commentId={comment.postId}
                    onDeletedPost={this.props.onDeletedComment}
                    creator={comment.creatorNickname}
                    creatorId={comment.creatorId}
                    creationDate={comment.creationDate}
                    activeUserId={this.props.activeUserId}
                    key={comment.postId}>
                    {post.content}
                </Comment>
            );
        }.bind(this));
        return (
            <div className="comment-list">
                {commentNodes}
            </div>
        );
    }
}