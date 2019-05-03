import React, { Component } from 'react'
import classNames from 'classnames';
import { Waypoint } from 'react-waypoint';
import placeholder from '../../img/placeholder.png';

export default class Image extends Component {
    constructor(props) {
        super(props);
        this.state = {
            loaded: false,
            src: this.placeholderSrc(props.img)
        };
        this.loadImage = this.loadImage.bind(this);
    }

    loadImage() {
        if (this.props.img.url !== '') {
            this.setState({
                src: this.props.img.url,
                loaded: true
            });
        } else {
            this.setState({
                src: placeholder,
                loaded: true
            });
        }
    }

    placeholderSrc(img) {
        let { width, height } = img;
        return `data:image/svg+xml,%3Csvg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 ${width} ${height}"%3E%3C/svg%3E`;
    }

    render() {
        const { className } = this.props;
        const { url, title } = this.props.img;
        const { src } = this.state;

        var imgClass = classNames({
            'block': true,
            [className]: true
        });

        return (
            <div className="w-full h-full min-h-full block bg-grey-light rounded">
                <Waypoint onEnter={this.loadImage} threshold={2.0}>
                    <img src={src} data-src={url} alt={title} className={imgClass} />
                </Waypoint>
            </div>
        );
    }
}
