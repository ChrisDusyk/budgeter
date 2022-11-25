import { useAuth0 } from "@auth0/auth0-react";

export function Profile() {
	const { user, isAuthenticated, isLoading } = useAuth0();

	if (isLoading) {
		return <div>Loading...</div>;
	}

	return <>{isAuthenticated && <div>Hello {user?.name}</div>}</>;
}
