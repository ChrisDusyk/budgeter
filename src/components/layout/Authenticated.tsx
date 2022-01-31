import { Box } from "grommet";
import { LogoutButton, Profile } from "..";

export function Authenticated() {
	return (
		<Box justify="center" align="center">
			<Profile />
			<LogoutButton />
		</Box>
	);
}
