import React, { Component, Fragment } from 'react';
import classNames from 'classnames'
import Dropzone from 'react-dropzone'
import Button from '../Base/Button';

export default class Upload extends Component {
	constructor(props) {
		super(props)

		this.state = {
			descriptionLimit: 280,
			maxLimit: 280,
			files: []
		};

		this.handleWordCount = this.handleWordCount.bind(this);
	}

	onPreviewDrop = (files) => {
		this.setState({
			files: this.state.files.concat(files),
		});
	}

	handleWordCount = event => {
		const charCount = event.target.value.length;
		const charLeft = this.state.maxLimit - charCount;
		this.setState({ descriptionLimit: charLeft });
	}

	render() {
		const previewStyle = {
			display: 'inline',
			width: 100,
			height: 100,
		};

		return (
			<div className="flex flex-wrap -mx-2">
				<div className="w-full px-2">
					<h1 className="text-2xl mb-4">Upload</h1>

					<div className="form-group">
						<label htmlFor="price" className="form-label">Select photos for publishing</label>

						<Dropzone accept="image/*" onDrop={this.onPreviewDrop}>
							{({ getRootProps, getInputProps, isDragActive }) => {
								return (
									<section>
										<div
											{...getRootProps({
												onClick: event => event.stopPropagation(),
											})}
											className={classNames('dropzone', { 'active': isDragActive })}
										>
											<input {...getInputProps()} />
											{
												<div className="text-center">
													<span className="mr-3">Drop your photos here or </span>
													<Button htmlType="button">
														Browse
													</Button>
												</div>
											}
										</div>
									</section>
								)
							}}
						</Dropzone>

						{this.state.files.length > 0 &&
							<Fragment>
								{this.state.files.map((file, index) => (
									<img
										alt="Preview"
										key={index}
										src={file.preview}
										style={previewStyle}
									/>
								))}
							</Fragment>
						}
					</div>

					<div className="form-group">
						<label htmlFor="title" className="form-label">Title</label>
						<input type="text" placeholder="Name of product" name="title" className="form-input" />
					</div>

					<div className="form-group">
						<label htmlFor="price" className="form-label">Price</label>
						<input type="text" placeholder="0+" name="price" className="form-input" />
					</div>

					<div className="form-group">
						<label htmlFor="description" className="form-label">Description</label>
						<textarea name="description" rows="4" placeholder="Describe your product&hellip;" className="form-input" onChange={this.handleWordCount.bind(this)} maxLength={this.state.maxLimit}></textarea>
						<p className="form-text">{this.state.descriptionLimit} characters left</p>
					</div>

					<Button type="primary">Create product</Button>
				</div>
			</div>
		);
	}
}
