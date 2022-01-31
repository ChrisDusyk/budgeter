import { Box } from "grommet";
import { LogoutButton, Profile } from "..";

export function AuthenticatedView() {
	return (
		<Box align="start" justify="start" border={{ color: "brand", size: "medium" }}>
			<Profile />
			<LogoutButton />
		</Box>
	);
}
