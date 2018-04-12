import React from 'react';

export default class PostForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            text: ''
        };
    }

    handleTextChange(e) {
        this.setState({ text: e.target.value });
    }

    handleSubmit(e) {
        e.preventDefault();
        var text = this.state.text.trim();
        if (!text) {
            return;
        }
        var data = new FormData();
        data.append('text', text);

        var xhr = new XMLHttpRequest();
        xhr.open('post', "posts/add", true);
        xhr.onload = function () {
            this.setState({ text: '' });
            this.props.onAddedPost();
        }.bind(this);
        xhr.send(data);
        
    }

    render() {
        return (
            <form className="post-form" onSubmit={this.handleSubmit.bind(this)} >
                <textarea className="form-control" rows="5"
                    type="text"
                    placeholder="Say something..."
                    value={this.state.text}
                    onChange={this.handleTextChange.bind(this)}
                />
                <input className="post-button btn-success" type="submit" value="Post" />
            </form>
        );
    }
}