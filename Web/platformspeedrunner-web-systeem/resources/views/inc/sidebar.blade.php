<!-- Navbar -->
<nav class="main-header navbar navbar-expand navbar-white navbar-light">
    <!-- Left navbar links -->
    <ul class="navbar-nav">
        <li class="nav-item">
            <a class="nav-link" data-widget="pushmenu" href="#" role="button"><i class="fas fa-bars"></i></a>
        </li>
    </ul>
</nav>
<!-- /.navbar -->

<!-- Main Sidebar Container -->
<aside class="main-sidebar sidebar-dark-primary elevation-4">
    <!-- Brand Logo -->
    <a href="/" class="brand-link" style="padding-bottom: 30px">
        <img src="{{ asset('/img/logo.png') }}" alt="Platformer Speedrunner" class="brand-image" style="max-width: 200px;"/>
        <span class="brand-text font-weight-light"></span>
    </a>

    <!-- Sidebar -->
    <div class="sidebar">
        @if (auth()->user())
        <!-- Sidebar user panel (optional) -->
        <div class="user-panel mt-3 pb-3 mb-3 d-flex">
            <div class="info">
                <a class="d-block">{{ auth()->user()->username }}</a>
            </div>
        </div>
        @endif
        <!-- Sidebar Menu -->
        <nav class="mt-2">
            <ul class="nav nav-pills nav-sidebar flex-column" data-widget="treeview" role="menu" data-accordion="false">
                <li class="nav-item">
                    <a href="{{ route('leaderboard') }}" class="nav-link">
                        <i class="nav-icon fas fa-th-list"></i>
                        <p>
                            Leaderboard
                        </p>
                    </a>
                </li>
                @if(auth()->user())
                    <li class="nav-item">
                        <a href="{{ route('personal_runs') }}" class="nav-link">
                            <i class="nav-icon fas fa-running"></i>
                            <p>
                                Personal Runs
                            </p>
                        </a>
                    </li>
                    <li class="nav-item">
                        <a href="{{ route('comment.index') }}" class="nav-link">
                            <i class="nav-icon fas fa-comments"></i>
                            <p>
                                Personal Comments
                            </p>
                        </a>
                    </li>
                @endif

                @if((new App\Http\Controllers\AuthenticatorController)->IsAdmin())
                <li class="nav-item has-treeview">
                    <a href="#" class="nav-link">
                        <i class="nav-icon fas fa-database"></i>
                        <p>
                            Data management
                            <i class="right fas fa-angle-left"></i>
                        </p>
                    </a>
                    <ul class="nav nav-treeview">
                        <li class="nav-item">
                            <a href="{{ route('user.index') }}" class="nav-link">
                                <i class="nav-icon fas fa-users"></i>
                                <p>
                                    Users
                                </p>
                            </a>
                        </li>
                        <li class="nav-item">
                            <a href="{{ route('run.index') }}" class="nav-link">
                                <i class="nav-icon fas fa-running"></i>
                                <p>
                                    Runs
                                </p>
                            </a>
                        </li>
                        <li class="nav-item">
                            <a href="{{ route('comment.index') }}" class="nav-link">
                                <i class="nav-icon fas fa-comments"></i>
                                <p>
                                    Comments
                                </p>
                            </a>
                        </li>
                        <li class="nav-item">
                            <a href="{{ route('role.index') }}" class="nav-link">
                                <i class="nav-icon fas fa-user-tag"></i>
                                <p>
                                    Roles
                                </p>
                            </a>
                        </li>
                    </ul>
                </li>
                @endif

                @if (auth()->user())
                <li class="nav-item">
                    <a class="nav-link" href="{{ route('logout') }}">
                        <i class="nav-icon fas fa-sign-out-alt"></i>
                        <p>
                            Logout
                        </p>
                    </a>
                    <form id="logout-form" action="{{ route('logout') }}" method="GET" style="display: none;">
                        @csrf
                    </form>
                    @else
                    <li class="nav-item">
                        <a class="nav-link" href="{{ route('login') }}">
                            <i class="nav-icon fas fa-sign-in-alt"></i>
                            <p>
                                Login
                            </p>
                        </a>
                    @endif
                </li>
            </ul>
        </nav>
        <!-- /.sidebar-menu -->
    </div>
    <!-- /.sidebar -->
</aside>
