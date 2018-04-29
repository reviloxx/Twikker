import React from 'react';
import * as ajaxhandler from '../ajax-handler';

export default class CommentForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            content: ''
        };
    }

    handleTextChange(e) {
        this.setState({ content: e.target.value });
    }

    handleSubmit(e) {
        e.preventDefault();
        var text = this.state.content.trim();
        if (!text) {
            return;
        }
        var data = new FormData();
        data.append('content', text);
        data.append('postId', this.props.postId);

        ajaxhandler.ajaxRequest(data, 'comments/add', this.handleResponse.bind(this));
        var xhr = new XMLHttpRequest();
        xhr.open('post', "comments/add", true);
    }

    handleResponse(response) {
        if (response.successful) {
            this.setState({ content: '' });
            this.props.onAddedComment();
        } else {
            alert(response.responseData);
        }
    }

    render() {
        if (this.state.content.length > 0) {
            return (
                <form className="comment-form" onSubmit={this.handleSubmit.bind(this)} >
                    <textarea className="form-control" rows="2"
                        type="text"
                        maxLength={300}
                        placeholder="Say something..."
                        value={this.state.content}
                        onChange={this.handleTextChange.bind(this)}
                    />
                    <input className="reply-button btn-info" type="submit" value="Send" />
                    <p className="char-counter">
                        {300 - this.state.content.length}
                    </p>
                </form>
            );
        } else {
            return (
                <form className="comment-form" onSubmit={this.handleSubmit.bind(this)} >
                    <textarea className="form-control" rows="2"
                        type="text"
                        maxLength={300}
                        placeholder="Say something..."
                        value={this.state.content}
                        onChange={this.handleTextChange.bind(this)}
                    />
                </form>
            );
        }
    }
}
CommentForm.displayName = 'CommentForm';