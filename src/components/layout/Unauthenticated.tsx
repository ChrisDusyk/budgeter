import { Box, Heading } from "grommet";
import { LoginButton } from "..";

export function UnauthenticatedView() {
	return (
		<Box
			a11yTitle="You're unauthenticated! Please log in."
			pad="xlarge"
			justify="center"
			align="center"
			alignContent="center"
			direction="column"
			fill="vertical"
			border={{ color: "brand", size: "medium" }}>
			<Heading level={2}>Please log in to continue...</Heading>
			<br />
			<LoginButton />
		</Box>
	);
}
