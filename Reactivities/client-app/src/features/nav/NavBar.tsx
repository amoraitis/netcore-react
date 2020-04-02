import React from "react";
import { Menu, Container, Button } from "semantic-ui-react";

interface IProps{
  openCreateForm: () => void
}

export const NavBar: React.FC<IProps> = (props) => {
  return (
    <Menu fixed="top" inverted>
      <Container>
        <Menu.Item header>
        <img src="assets/logo.png" alt="logo" style={{marginRight: "1em"}}/>
        Reactivities
        </Menu.Item>
        <Menu.Item name="Activities" />
        <Menu.Item>
          <Button onClick={props.openCreateForm} positive content="Create Activity"></Button>
        </Menu.Item>
      </Container>
    </Menu>
  );
};
