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
    <a href="/" class="brand-link">
        <h4 class="text-center">PlatformSpeedRunner</h4>
    </a>

    <!-- Sidebar -->
    <div class="sidebar">
        @if (auth()->user())
        <!-- Sidebar user panel (optional) -->
            <div class="user-panel mt-3 pb-3 mb-3 d-flex">
                <div class="info" style="margin-bottom: -10%; margin-top:-3%">
                    <p class="d-block" style="color:lightgrey; margin-left: -2%">{{ auth()->user()->username }}</p>
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
                        <a href="{{ route('personal_comments') }}" class="nav-link">
                            <i class="nav-icon fas fa-comments"></i>
                            <p>
                                Personal Comments
                            </p>
                        </a>
                    </li>
                    @if(auth()->user()->admin)
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
                                    <a href="{{ route('link.index') }}" class="nav-link">
                                        <i class="nav-icon fas fa-link"></i>
                                        <p>
                                            Links
                                        </p>
                                    </a>
                                </li>
                                <li class="nav-item">
                                    <a href="{{ route('user.index') }}" class="nav-link">
                                        <i class="nav-icon fas fa-users"></i>
                                        <p>
                                            Users
                                        </p>
                                    </a>
                                </li>
                            </ul>
                        </li>
                    @endif
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
                    </li>
                @endif
            </ul>
        </nav>
        <!-- /.sidebar-menu -->
    </div>
    <!-- /.sidebar -->
</aside>
