import React, { Component } from 'react';
import { connect } from 'react-redux';
import { NavLink, Link, withRouter } from 'react-router-dom';
import styled from 'styled-components'
import SearchField from './SearchField';
import CartCount from './CartCount';
import { Button } from 'pikcha-frame';

const Header = styled.header`
`

const Nav = styled.nav`
    padding: 0.5rem 1rem;
    align-items: center;
    display: flex;
    position: relative;
    justify-content: flex-start;
    flex-flow: row nowrap;
`

class TopBar extends Component {
	constructor(props) {
		super(props)

		this.state = {
			isToggled: false
		};

		this.handleToggle = this.handleToggle.bind(this)
	}

	handleToggle = () => {
		this.setState({ isToggled: !this.state.isToggled });
	}

	componentDidUpdate(prevProps) {
		if (this.props.location !== prevProps.location) {
			this.setState({ isToggled: false });
		}
	}

	render() {
		const { isToggled } = this.state;

		return (
			<Header>
				<Nav>
					<div>
                        <span onClick={this.handleToggle.bind(this)}>
                            <div></div>
                            <div></div>
                            <div></div>
                        </span>

						<h1 itemScope="" itemType="http://schema.org/Organization">
							<Link to="/">
								Pikcha
                            </Link>
						</h1>
					</div>

					<div>
                        <SearchField />
                    </div>

                    <div>
                        <Button size="sm" to="/upload" mr={2} as={Link}>Submit a photo</Button>
                        <Button size="sm" kind="primary" to="/signup" as={Link}>Sign up</Button>
                        <CartCount />
					</div>
				</Nav>
			</Header>
		)
	}
}

export default withRouter(connect()(TopBar));
