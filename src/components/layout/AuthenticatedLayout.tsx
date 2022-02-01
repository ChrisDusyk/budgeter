import React from "react";
import { Box, Button, Header, Main, Nav } from "grommet";
import { Home } from "grommet-icons";
import { LogoutButton, Profile } from "..";

export const AuthenticatedLayout: React.FC<React.PropsWithChildren<unknown>> = ({ children }: React.PropsWithChildren<unknown>) => {
	return (
		<Box align="start" justify="start" border={{ color: "brand", size: "medium" }}>
			<>
				<Header background="light-4" fill="horizontal" pad="small">
					<Button icon={<Home />} hoverIndicator />
					<Nav direction="row">
						<Profile />
						<LogoutButton />
					</Nav>
				</Header>
				<Main border={{ color: "red", size: "medium" }} pad="medium">
					{children}
				</Main>
			</>
		</Box>
	);
};
