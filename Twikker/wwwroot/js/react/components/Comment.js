import React from 'react';

export default class Comment extends React.Component {
    //constructor(props) {
    //    super(props);
    //    this.state = { commentId: this }
    //}

    handleDeleteClick(e) {
        e.preventDefault();
        console.log("Delete Comment: " + this.props.commentId);
        var data = new FormData();
        data.append('commentId', this.props.commentId)

        var xhr = new XMLHttpRequest();
        xhr.open('post', "comments/delete", true);
        xhr.onload = function () {
        }.bind(this);
        xhr.send(data);
    }

    componentDidMount() {
        $(document).ready(function () {
            $('.comment').mouseenter(function () {
                $(this).children('.delete-button').fadeIn(100);
            });
            $('.comment').mouseleave(function () {
                $(this).children('.delete-button').fadeOut(100);
            });
        });
    };

    render() {
        console.log(this.props.activeUserId + ' ' + this.props.creatorId);
        if (this.props.activeUserId == this.props.creatorId) {
            return (
                <div className="comment">
                    <button className="delete-button btn-danger btn-sm" onClick={this.handleDeleteClick.bind(this)}>Delete</button>
                    <h4 className="creator">{this.props.creator}</h4>
                    <h6 className="creation-date">{this.props.creationDate}</h6>
                    <h6 className="comment-text">{this.props.children}</h6>
                </div>
            );
        } else {
            return (
                <div className="comment">
                    <h4 className="creator">{this.props.creator}</h4>
                    <h6 className="creation-date">{this.props.creationDate}</h6>
                    <h6 className="comment-text">{this.props.children}</h6>
                </div>
            );
        }
    }
}