import React, { Component, Fragment } from 'react';
import Dropzone from 'react-dropzone'
import { Button } from 'pikcha-frame';

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
			<div>
				<div>
					<h1 >Upload</h1>

					<div>
						<label htmlFor="price" >Select photos for publishing</label>

						<Dropzone accept="image/*" onDrop={this.onPreviewDrop}>
							{({ getRootProps, getInputProps, isDragActive }) => {
								return (
									<section>
										<div
											{...getRootProps({
												onClick: event => event.stopPropagation(),
											})}
										>
											<input {...getInputProps()} />
											{
												<div>
													<span>Drop your photos here or </span>
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

					<div>
						<label htmlFor="title" >Title</label>
						<input type="text" placeholder="Name of product" name="title"  />
					</div>

					<div>
						<label htmlFor="price" >Price</label>
						<input type="text" placeholder="0+" name="price"  />
					</div>

					<div>
						<label htmlFor="description" >Description</label>
						<textarea name="description" rows="4" placeholder="Describe your product&hellip;"  onChange={this.handleWordCount.bind(this)} maxLength={this.state.maxLimit}></textarea>
						<p>{this.state.descriptionLimit} characters left</p>
					</div>

					<Button type="primary">Create product</Button>
				</div>
			</div>
		);
	}
}
