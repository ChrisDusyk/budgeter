import { Box, Heading } from "grommet";
import { LoginButton } from "..";

export function UnauthenticatedView() {
	return (
		<Box a11yTitle="You're unauthenticated! Please log in." align="center" justify="center">
			<Heading level={2}>Please log in to continue...</Heading>
			<LoginButton />
		</Box>
	);
}
