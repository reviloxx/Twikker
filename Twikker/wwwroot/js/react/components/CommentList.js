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
        var commentNodes = this.props.comments.map(function (comment) {
            return (
                <Comment commentId={comment.commentId}
                    onDeletedPost={this.props.onDeletedComment}
                    creator={comment.creatorNickname}
                    creatorId={comment.creatorId}
                    creationDate={comment.creationDate}
                    activeUserId={this.props.activeUserId}
                    key={comment.postId}>
                    {comment.content}
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